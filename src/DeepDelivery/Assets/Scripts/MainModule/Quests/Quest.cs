namespace MainModule
{
    public class Quest
    {
        private readonly QuestGiver _questGiver;

        public Quest(QuestGiver questGiver)
        {
            _questGiver = questGiver;
        }

        public bool IsQuestDestination(QuestTakeBehaviour questTakeBehaviour)
        {
            return _questGiver.IsQuestDestination(questTakeBehaviour);
        }
    }
}