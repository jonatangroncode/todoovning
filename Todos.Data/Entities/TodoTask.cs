using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Todos.Data.Entities;
public class TodoTasks
{
    [Key]
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int TodoId { get; set; }
    [ForeignKey("TodoId")]
    public Todo? Todo { get; set; }
}

