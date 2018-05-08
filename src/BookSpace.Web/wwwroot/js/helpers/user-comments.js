function addComment(id, userId) {
    let content = $("#comment-message").val()
    console.log(content)
    console.log(id)
    console.log(userId)
    $.ajax({
        type: "POST",
        url: "/Book/AddComment",
        data: { id: id, comment: content, userId: userId } ,
        success: (response) => {
            showSuccessMessage('Comment added')
            setTimeout(function () {
                location.reload();
            }, 2000);
        },
        error: (response) => {
            showFailureMessage('Cannot add comment.');
        }
    })
}