using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Borrow_App;
public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Sex { get; set; }
    public List<string> BorrowedBookTitles { get; set; }
}

