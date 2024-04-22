using AnonymousPoll.Interfaces;
using AnonymousPoll.Model;

namespace AnonymousPoll.ViewModels
{
    public class StudentsViewModel : CommonModel, IInitializable<string[]>
    {
        const byte splits = 2;

        //Dictionary can store student info as a key, having a SortedSet of students Names as value (to have lexicographical order)
        private readonly IDictionary<string, ISet<string>> students = new Dictionary<string, ISet<string>>();

        public void Initialize(string[] input)
        {
            Students.Clear();

            try
            {
                string[] splitItem = new string[splits];

                foreach (var item in input)
                {
                    splitItem = item.Split(",", splits);
                    if (Students.TryGetValue(splitItem[1], out var value))
                    {
                        value.Add(splitItem[0]);
                        continue;
                    }

                    Students.Add(splitItem[1], new SortedSet<string>() { splitItem[0] });
                }
            }
            catch
            {
                throw;
            }

            RaisePropertyChanged(nameof(Initialized));
        }

        public override string Title => "Students";

        public bool Initialized => Students.Any();

        public IDictionary<string, ISet<string>> Students => students;
    }
}
