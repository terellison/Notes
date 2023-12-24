using System.Collections.ObjectModel;

namespace Notes.Models;

internal class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    public AllNotes()
        => LodaNotes();

    public void LodaNotes()
    {
        Notes.Clear();

        string appDataPath = FileSystem.AppDataDirectory;

        var notes = Directory
                        .EnumerateFiles(appDataPath, "*.notes.txt")
                        .Select(filename => new Note()
                        {
                            FileName = filename,
                            Text = File.ReadAllText(filename),
                            Date = File.GetCreationTime(filename)
                        })
                        .OrderBy(note => note.Date);
        
        foreach(var note in notes)
            Notes.Add(note);
    }
}
