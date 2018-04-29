<<<<<<< HEAD
﻿namespace BookSpace.Models
=======
﻿using BookSpace.Models.Enums;

namespace BookSpace.Models
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
{
    public class BookUser
    {
        public string BookId { get; set; }
<<<<<<< HEAD
        public BookDBModel Book { get; set; }
=======
        public Book Book { get; set; }
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

<<<<<<< HEAD
        public bool? IsRead { get; set; }
=======
        public BookState State { get; set; }
>>>>>>> 280e0ded4b43c1723fcd4027699ec9ba290e71ec
    }
}
