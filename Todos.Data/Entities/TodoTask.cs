
namespace Todos.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    public class TodoTasks
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public int TodoId { get; set; }
        [ForeignKey("TodoId")]
        public Todo Todo { get; set; }
    }

