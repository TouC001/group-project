using SoftwareBookList.GoogleBooks;
using SoftwareBookList.Models;

namespace SoftwareBookList.Model_View
{
    public class Book_CommentViewMmodel
    {
        public AddCommentViewModel addComment {  get; set; }

        public GoogleBook googleBook { get; set; }

        public List<Comment> comments { get; set; }

    }
}
