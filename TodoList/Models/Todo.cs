using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Models;

public class Todo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public bool IsFinish { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
