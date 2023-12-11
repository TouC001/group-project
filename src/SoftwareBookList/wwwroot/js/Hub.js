var connection = new signalR.HubConnectionBuilder().withUrl("/bookHub").build();

let bookElement = document.getElementById("allTabContent");

let bookID = bookElement.dataset.BookID;
let bookListID = bookElement.dataset.BookListID;

connection.start()
    .then(function () {
        console.log("connected");
        connection.invoke("RemoveButton", bookID, bookListID);
    })
    .catch(function (err) { return console.error(err.toString()); });

let bookRows = document.querySelectorAll(".Books");

for (let bookRow of bookRows)
{
    bookRow.querySelector(".RemoveButton").addEventListener("click", function (event) {
        let bookID = bookRow.dataset.BookID;
        let bookListID = bookRow.dataset.BookListID;

        connection.invoke("RemoveButton", bookID, bookListID).catch(err => console.error(err.toString()));
    });
}
