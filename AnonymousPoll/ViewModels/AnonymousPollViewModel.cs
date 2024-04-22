using AnonymousPoll.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace AnonymousPoll.ViewModels
{
    public class AnonymousPollViewModel : BindableBase
    {
        public AnonymousPollViewModel()
        {
            this.Students = new StudentsViewModel();
            this.Input = new InputViewModel();
            this.Output = new OutputViewModel();

            this.LoadStudentsCommand = new DelegateCommand(async () =>
            {
                var result = await FileDialogHelper.OpenFileAndReadLinesAsync();

                try
                {
                    this.Students.Initialize(result);
                    this.RaisePropertyChanged(nameof(this.CanCalculateOutput));
                }
                catch
                {
                    MessageBox.Show("An error ocurried while opening Students file. Please try again.");
                }
            });

            this.LoadInputCommand = new DelegateCommand(async () =>
            {
                var result = await FileDialogHelper.OpenFileAndReadLinesAsync();

                try
                {
                    this.Input.Initialize(result);
                    this.RaisePropertyChanged(nameof(this.CanCalculateOutput));
                }
                catch
                {
                    MessageBox.Show("An error ocurried while opening Input file. Please try again.");
                }
            });

            this.CalculateOutputCommand = new DelegateCommand(async () =>
            {
                var result = this.Output.Execute(this.Students, this.Input);

                try
                {
                    await FileDialogHelper.WriteFileAsync(result);
                }
                catch
                {
                    MessageBox.Show("An error ocurried while saving Output file. Please try again.");
                }
            });
        }

        public StudentsViewModel Students { get; private set; }
        public InputViewModel Input { get; private set; }
        public OutputViewModel Output { get; private set; }

        public bool CanCalculateOutput => this.Students.Initialized && this.Input.Initialized;

        public ICommand LoadStudentsCommand { get; private set; }
        public ICommand LoadInputCommand { get; private set; }
        public ICommand CalculateOutputCommand { get; private set; }
    }
}
