using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookSpace.Data.Contracts;
using BookSpace.Data.DTO;
using BookSpace.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace BookSpace.Data
{
    public class DatabaseSeedService : IDatabaseSeedService
    {
        private readonly BookSpaceContext ctx;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public DatabaseSeedService(BookSpaceContext ctx, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.ctx = ctx;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task SeedDataAsync()
        {
            await this.SeedBooksAsync();
            await this.SeedGenresAsync();
            await this.SeedRolesAsync();
            await this.SeedUsersAsync();
            await this.SeedCommentsAsync();
            await this.SeedGenresBooksAsync();
            await this.SeedBookUsersAsync();
            await this.SeedTagsAsync();
            await this.SeedBookTags();
        }

        private async Task SeedBookTags()
        {
            if (!ctx.BooksTags.Any())
            {
                string[] tagsIds = await this.ctx.Tags.Select(i => i.TagId).ToAsyncEnumerable().ToArray();
                string[] bookIds = await this.ctx.Books.Select(i => i.BookId).ToAsyncEnumerable().ToArray();

                List<BookTag> bookTags = new List<BookTag>();

                // For each book add 3 tags
                int chunkSize = tagsIds.Length / 3;
                Random rnd = new Random();
                for (int i = 0; i < tagsIds.Length; i++)
                {
                    bookTags.Add(new BookTag()
                    {
                        BookId = bookIds[i],
                        TagId = tagsIds[rnd.Next(0, chunkSize)]
                    });

                    bookTags.Add(new BookTag()
                    {
                        BookId = bookIds[i],
                        TagId = tagsIds[rnd.Next(0 + chunkSize, chunkSize * 2)]
                    });

                    bookTags.Add(new BookTag()
                    {
                        BookId = bookIds[i],
                        TagId = tagsIds[rnd.Next(0 + 2 * chunkSize, chunkSize * 3)]
                    });
                }

                await ctx.BooksTags.AddRangeAsync(bookTags);
                await ctx.SaveChangesAsync();
            }
        }

        private async Task SeedTagsAsync()
        {
            if (!ctx.Tags.Any())
            {
                string responseBody = await this.ReadJsonAsync("https://academystorage18.blob.core.windows.net/unknown/Tags.json?sv=2017-07-29&sr=b&sig=aNbPcDqhFTrwwB2qDhTIP4UlUEj6dq%2BspzQbyI6KR2k%3D&se=9999-12-31T21%3A59%3A59Z&sp=rd");
                Tag[] allTags = JsonConvert.DeserializeObject<Tag[]>(responseBody);

                await ctx.Tags.AddRangeAsync(allTags);
                await ctx.SaveChangesAsync();
            }
        }

        private async Task SeedBookUsersAsync()
        {
            if (!ctx.BooksUsers.Any())
            {
                string[] bookIds = ctx.Books.Select(b => b.BookId).ToArray();
                string[] userIds = ctx.Users.Select(u => u.Id).ToArray();

                // For each user add 2 books with different state
                List<BookUser> bookUsers = new List<BookUser>();
                Random rnd = new Random();
                foreach (var uId in userIds)
                {
                    bookUsers.Add(new BookUser
                    {
                        UserId = uId,
                        BookId = bookIds[rnd.Next(0, bookIds.Length / 2)],
                        State = Models.Enums.BookState.Read
                    });

                    bookUsers.Add(new BookUser
                    {
                        UserId = uId,
                        BookId = bookIds[rnd.Next(bookIds.Length / 2, bookIds.Length)],
                        State = Models.Enums.BookState.ToRead
                    });
                }

                await ctx.BooksUsers.AddRangeAsync(bookUsers);
                await ctx.SaveChangesAsync();
            }
        }

        private async Task SeedGenresBooksAsync()
        {
            if (!ctx.BooksGenres.Any())
            {
                string[] bookIds = ctx.Books.Select(b => b.BookId).ToArray();
                string[] genreIds = ctx.Genres.Select(b => b.GenreId).ToArray();

                List<BookGenre> booksGenres = new List<BookGenre>();
                Random rnd = new Random();
                foreach (var bId in bookIds)
                {
                    booksGenres.Add(new BookGenre
                    {
                        BookId = bId,
                        GenreId = genreIds[rnd.Next(0, genreIds.Length)]
                    });
                }

                await ctx.BooksGenres.AddRangeAsync(booksGenres);
                await ctx.SaveChangesAsync();
            }
        }

        private async Task SeedCommentsAsync()
        {
            if (!ctx.Comments.Any())
            {
                string responseBody = await this.ReadJsonAsync("https://academystorage18.blob.core.windows.net/unknown/Comments.json?sv=2017-07-29&sr=b&sig=uLLXu01htpg8zgc0y5B6n%2BLzU4lSY%2BPSYse5MjTgeSg%3D&se=9999-12-31T21%3A59%3A59Z&sp=rd");
                Comment[] allComments = JsonConvert.DeserializeObject<Comment[]>(responseBody);

                string[] usersIds = ctx.Users.Select(i => i.Id).ToArray();
                string[] bookIds = ctx.Books.Select(b => b.BookId).ToArray();

                Random rnd = new Random();

                foreach (var comment in allComments)
                {
                    comment.UserId = usersIds[rnd.Next(0, usersIds.Length)];
                    comment.BookId = bookIds[rnd.Next(0, bookIds.Length)];
                }

                await ctx.Comments.AddRangeAsync(allComments);
                await ctx.SaveChangesAsync();
            }
        }

        private async Task SeedUsersAsync()
        {
            if (!this.ctx.Users.Any())
            {
                // Seed admin
                var adminEmail = "admin@admin.com";
                var adminUsername = adminEmail;
                var adminPassword = "Asd12@";
                this.CreateUser(adminEmail, adminUsername, adminPassword);
                this.SetRoleToUser("admin@admin.com", "Admin");

                // Add some users
                string responseBody = this.ReadJsonAsync("https://academystorage18.blob.core.windows.net/unknown/Users.json?sv=2017-07-29&sr=b&sig=YxSCb%2BO4HLa569eyyW8Gbm4pr7p3B5Mp9%2BuXkE2S8Es%3D&se=9999-12-31T21%3A59%3A59Z&sp=rd").Result;
                var allUsers = JsonConvert.DeserializeObject<UserSeedDto[]>(responseBody);

                foreach (var user in allUsers)
                {
                    this.CreateUser(user.Email, user.Username, "Asd12@");
                    this.SetRoleToUser(user.Email, "User");
                }
                
                await ctx.SaveChangesAsync();
            }
        }

        private async Task SeedRolesAsync()
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "User"
                };

                IdentityResult roleResult = await roleManager.CreateAsync(role);
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityResult roleResult = await roleManager.CreateAsync(role);
            }
        }

        private async Task SeedGenresAsync()
        {
            if (!ctx.Genres.Any())
            {
                string responseBody = this.ReadJsonAsync("https://academystorage18.blob.core.windows.net/unknown/Genres.json?sv=2017-07-29&sr=b&sig=CJ74O0PbWVZoecuXRF9UFagG9mBdp%2BB6RRU2SK5CeCA%3D&se=9999-12-31T21%3A59%3A59Z&sp=rd").Result;
                Genre[] allGenres = JsonConvert.DeserializeObject<Genre[]>(responseBody);
                await ctx.Genres.AddRangeAsync(allGenres);
                await ctx.SaveChangesAsync();
            }
        }

        private async Task SeedBooksAsync()
        {
            if (!ctx.Books.Any())
            {
                string responseBody = this.ReadJsonAsync("https://academystorage18.blob.core.windows.net/unknown/Books.json?sv=2017-07-29&sr=b&sig=ikTp51MPPEOCowkqL7hym8sGGycWlDN0Ztw2JgeN5%2Fg%3D&se=9999-12-31T21%3A59%3A59Z&sp=rd").Result;
                Book[] allBooks = JsonConvert.DeserializeObject<Book[]>(responseBody);

                await ctx.Books.AddRangeAsync(allBooks);
                await ctx.SaveChangesAsync();
            }
        }

        private async Task<string> ReadJsonAsync(string uri)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        private void CreateUser(string email, string username, string password)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = username,
                Email = email,
            };

            var result = userManager.CreateAsync(user, password).Result;

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }

        private void SetRoleToUser(string email, string role)
        {
            var user = ctx.Users.SingleOrDefault(u => u.Email == email);

            var result = userManager.AddToRoleAsync(user, role).Result;

            if (role == "User")
            {
                user.LockoutEnabled = true;
            }

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }
    }
}
