using System.IO;
using System.Linq;
using EXCSLA.Shared.Core;
using EXCSLA.Shared.Core.Exceptions;

namespace EXCSLA.Shared.Core.ValueObjects.Common
{
    /// <summary>
    /// A standard windows based file name. Because this is a 
    /// value object, any changes to this object should result in a new object creation. Thus there is no
    /// public setting of properties.
    /// </summary>
    public class FileName : ValueObject
    {
        public string Name {get; private set;}
        public string Extension {get; private set;}

        /// <summary>
        /// This is an entity framework required constructor, and should not be used by the programmer. Because
        /// this is a value object there is not way to set the values of its properties, making this constructor 
        /// usesless to anyone other than ORM's.
        /// </summary>
        public FileName() { } // Required by EF

        /// <summary>
        /// This creates a standard windows based filename.
        /// </summary>
        /// <param name="fileName">A string containing the filename along with the extension.</param>
        public FileName(string fileName)
        {
            if(fileName.Length > 250) throw new FileNameMalformedException("FileName must be less than 250 characters.");
            if(fileName.All(f => Path.GetInvalidFileNameChars().Contains(f))) throw new FileNameMalformedException("Invalide File Name");

            int nameStartIndex = 0;

            if(fileName.LastIndexOf('/') == -1)
            {
                if(fileName.LastIndexOf('\\') == -1) nameStartIndex = 0;
                else nameStartIndex = fileName.LastIndexOf('\\') + 1;
            }
            else
                nameStartIndex = fileName.LastIndexOf('/') + 1;

            int extensionStartIndex = fileName.LastIndexOf('.') + 1;

            this.SetName(fileName.Substring(nameStartIndex, ((extensionStartIndex - 1) - nameStartIndex)));
            this.SetExtension(fileName.Substring(extensionStartIndex));
        }

        private void SetName(string name)
        {
            this.Name = name;
        }

        private void SetExtension(string extension)
        {
            this.Extension = extension;
        }

        public override string ToString()
        {
            return this.Name + "." + this.Extension;
        }

    }
}