namespace MainModule
{
    public class QuestFactory : IFactory<QuestGiver, QuestGiveBehaviour>, IFactory<QuestTaker, QuestTakeBehaviour>
    {
        public QuestGiver Create(QuestGiveBehaviour data)
        {
            return new QuestGiver(data, this);
        }

        public QuestTaker Create(QuestTakeBehaviour data)
        {
            return new QuestTaker(data);
        }

        public Quest Create(QuestGiver data)
        {
            return new Quest(data);
        }
    }
}