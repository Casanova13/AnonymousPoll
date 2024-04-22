using AnonymousPoll.Model;

namespace AnonymousPoll.ViewModels
{
    public class OutputViewModel : CommonModel
    {
        private int count;

        private void ResetStatus()
        {
            this.count = 1;
        }

        private string CaseBuilder => $"Case #{this.count++}: ";

        private string SentenceBuilder(string value) => this.CaseBuilder + value;

        public IEnumerable<string> Execute(StudentsViewModel studentsWrapper, InputViewModel inputViewModel)
        {
            this.ResetStatus();

            foreach (var input in inputViewModel.Inputs)
            {
                if (studentsWrapper.Students.TryGetValue(input, out var value))
                {
                    yield return this.SentenceBuilder(string.Join(",", value));
                    continue;
                }

                yield return this.SentenceBuilder("NONE");
            }
        }

        public override string Title => "Output";
    }
}
