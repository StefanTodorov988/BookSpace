function addRead(id, title) {
    $.ajax({
        type: "POST",
        url: "/UserBooks/AddBook",
        data: { id: id, collection: 'Read' },
        success: (response) => {
            showSuccessMessage('You have successfully added ' + title + ' to Read collection.')
        },
        error: (response) => {
            showFailureMessage(title + ' is already in your Read collection');
        }
    })
}

function addToRead(id, title) {
    $.ajax({
        type: "POST",
        url: "/UserBooks/AddBook",
        data: { id: id, collection: 'ToRead' },
        success: (response) => {
            showSuccessMessage('You have successfully added ' + title + ' to Want to Read collection.')
        },
        error: (response) => {
            showFailureMessage(title + ' is already in your  Want to Read collection');
        }
    })
}

function addCurrentyReading(id, title) {
    $.ajax({
        type: "POST",
        url: "/UserBooks/AddBook",
        data: { id: id, collection: 'CurrentlyReading' },
        success: (response) => {
            showSuccessMessage('You have successfully added ' + title + ' to Currently Reading collection.')
        },
        error: (response) => {
            showFailureMessage(title + ' is already in your Currently Reading collection');
        }
    })
}