using Microsoft.AspNetCore.Components.Forms;

using System.Diagnostics.Eventing.Reader;

namespace Company_Site.Helpers
{
	public class UploadedFile : IBrowserFile
	{
        #region Private Members

        private string _name;

        private DateTimeOffset _lastModified;

        private long _size;

        private string _contentType;

        private string _filePath;

        #endregion

        #region Public Properties

        public string Name => _name;

        public DateTimeOffset LastModified => _lastModified;

        public long Size => _size;

        public string ContentType => _contentType;

        #endregion

        #region Public Methods

        public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
        {
            return new FileStream(_filePath, FileMode.Open);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filePath"></param>
        public UploadedFile(string filePath)
        {
            _filePath = filePath;
            FileInfo info = new FileInfo(_filePath);
            _name = info.Name;
            _lastModified = info.LastWriteTime;
            _size = info.Length;
            switch (info.Name.Split('.').Last())
            {
                case "pdf":
                    _contentType = "application.pdf";
                    break;
                case "png":
                case "jpg":
                case "jpeg":
                    _contentType = "image/png";
                    break;
                default:
                    _contentType = "application/document";
                    break;
            }
        }

        #endregion
    }
}
