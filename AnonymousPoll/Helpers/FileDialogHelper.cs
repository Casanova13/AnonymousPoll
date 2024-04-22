using Microsoft.Win32;
using System.IO;

namespace AnonymousPoll.Helpers
{
    public static class FileDialogHelper
    {
        private readonly static string defaultExt = ".txt";

        private readonly static OpenFileDialog openFileDialog;
        private readonly static SaveFileDialog saveFileDialog;

        static FileDialogHelper()
        {
            openFileDialog = new OpenFileDialog() { DefaultExt = defaultExt };
            saveFileDialog = new SaveFileDialog() { DefaultExt = defaultExt };
        }

        public static async Task<string[]> OpenFileAndReadLinesAsync()
        {
            if (openFileDialog.ShowDialog() == true)
            {
                return await File.ReadAllLinesAsync(openFileDialog.FileName);
            }

            return Array.Empty<string>();
        }

        public static async Task WriteFileAsync(IEnumerable<string> content)
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    await File.WriteAllLinesAsync(saveFileDialog.FileName, content);
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
