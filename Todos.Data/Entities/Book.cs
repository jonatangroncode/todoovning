using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Todos.Data.Entities;



public class Book
{


    public int Id { get; set; } = 0;
    public string Title { get; set; } = string.Empty;

    public int PublisherId { get; set; } //FK
    [ForeignKey("PublisherId")] public Publisher? Publisher { get; set; }

    public List<Author>? Authors { get; set; }





}
