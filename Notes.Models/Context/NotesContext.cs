using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notes.Models.Database.AdminModels;
using Notes.Models.Database.NotesModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.Context;

public class NotesContext : IdentityDbContext<User, Role, string>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<NoteItem> NoteItems { get; set; }

    public NotesContext(DbContextOptions<NotesContext> opts) : base(opts)
        => Database.EnsureCreated();
}
