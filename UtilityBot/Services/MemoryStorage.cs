using System.Collections.Concurrent;
using UtilityBot.Model;

namespace UtilityBot.Services
{
    public class MemoryStorage : IStorage
    {
        /// <summary>
        /// Хранилище сессий
        /// </summary>
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            var newSession = new Session() { CountType = CountType.SumSymbols };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
