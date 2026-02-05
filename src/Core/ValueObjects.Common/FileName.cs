using System.Linq;
using Ardalis.GuardClauses;
using EXCSLA.Shared.Core;
using EXCSLA.Shared.Core.Exceptions;

namespace MT.Core.ValueObjects;

public class FileName : ValueObject
{
    public string Name {get; private set;}
    public string Extension {get; private set;}
    public string Path { get; private set; }

    public FileName() { } // Required by EF

    public FileName(string fileName)
    {
        if(fileName.Length > 250) throw new FileNameMalformedException("FileName must be less than 250 characters.");
        if(fileName.All(f => System.IO.Path.GetInvalidFileNameChars().Contains(f))) throw new FileNameMalformedException("Invalide File Name");

        int nameStartIndex = 0;

        if(fileName.LastIndexOf('/') == -1)
        {
            if(fileName.LastIndexOf('\\') == -1) nameStartIndex = 0;
            else nameStartIndex = fileName.LastIndexOf('\\') + 1;
        }
        else
            nameStartIndex = fileName.LastIndexOf('/') + 1;

        int extensionStartIndex = fileName.LastIndexOf('.') + 1;

        if(nameStartIndex > 0) this.SetPath(fileName.Substring(0, nameStartIndex));
        this.SetName(fileName.Substring(nameStartIndex, ((extensionStartIndex - 1) - nameStartIndex)));
        this.SetExtension(fileName.Substring(extensionStartIndex));
    }

    private void SetName(string name)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));

        this.Name = name;
    }

    private void SetExtension(string extension)
    {
        Guard.Against.NullOrWhiteSpace(extension, nameof(extension));

        this.Extension = extension;
    }

    private void SetPath(string path)
    {
        this.Path = path;
    }

    public override string ToString()
    {
        return this.Path + this.Name + "." + this.Extension;
    }

    public string GetFileName()
    {
        return this.Name + "." + this.Extension;
    }

}