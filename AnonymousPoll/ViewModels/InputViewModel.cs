using AnonymousPoll.Interfaces;
using AnonymousPoll.Model;

namespace AnonymousPoll.ViewModels
{
    public class InputViewModel : CommonModel, IInitializable<string[]>
    {
        private int numberOfLines;
        private string[] inputs = Array.Empty<string>();

        public void Initialize(string[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            //First line must be the number of inputs, non-negative and can't exceed 100
            if (!int.TryParse(input[0], out var numberOfLines) || this.NumberOfLines < 0 || this.NumberOfLines > 100)
            {
                throw new FormatException();
            }

            this.NumberOfLines = numberOfLines;
            this.Inputs = new string[this.NumberOfLines];

            try
            {
                for (int i = 0; i < this.NumberOfLines; i++)
                {
                    this.Inputs[i] = input[i + 1];
                }
            }
            catch
            {
                throw new ArgumentOutOfRangeException();
            }

            this.RaisePropertyChanged(nameof(this.Initialized));
        }

        public override string Title => "Input";

        public bool Initialized => this.Inputs.Any();

        public int NumberOfLines
        {
            get => this.numberOfLines;
            set => this.SetProperty(ref this.numberOfLines, value);
        }

        public string[] Inputs
        {
            get => this.inputs;
            set => this.SetProperty(ref this.inputs, value);
        }
    }
}
