using Prism.Mvvm;
using System.IO;

namespace AnonymousPoll.Model
{
    public abstract class CommonModel : BindableBase
    {
        public abstract string Title { get; }
        public string Image => $"{AppDomain.CurrentDomain.BaseDirectory}/Resources/Images/{this.Title}.png";
    }
}
