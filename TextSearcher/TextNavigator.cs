using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextSearcher
{
    class TextNavigator
    {
        private TextFileWorker _textFileWorker;
        private List<int> _matchIndexes;
        char[] endCharacters = { ' ', '.', ',', '?', '!', '"', ':', '(', ')', ';', '\'', '\n', '\r', '\a', '\b', '\t', '\v', '\f', '»', '«', ' ' };
        public int CurrentWordPosition { get; set; }
        public int CurrentWordIndex { get; set; }
        public int CurrentWordLength 
        { 
            get
            {
                int wordLength = _textFileWorker.mainText.IndexOfAny(endCharacters, CurrentWordIndex) - CurrentWordIndex;
                if (wordLength == 0) wordLength = _textFileWorker.mainText.IndexOfAny(endCharacters, 1);
                return wordLength;
            }
        }
        public int PreviousWordIndex { get; set; }
        public int PreviousWordLength
        {
            get
            {
                int wordLength = _textFileWorker.mainText.IndexOfAny(endCharacters, PreviousWordIndex) - PreviousWordIndex;
                if (wordLength == 0) wordLength = _textFileWorker.mainText.IndexOfAny(endCharacters, 1);
                return wordLength;
            }
        }

        public TextNavigator(TextFileWorker fileWorker)
        {
            _textFileWorker = fileWorker;
            _matchIndexes = _textFileWorker.GetAllMatchIndexes();
            ResetPosition();
        }

        public void MoveNext()
        {
            PreviousWordIndex = CurrentWordIndex;
            CurrentWordPosition = CurrentWordPosition + 1 >= _matchIndexes.Count ? 0 : CurrentWordPosition + 1;
            CurrentWordIndex = _matchIndexes[CurrentWordPosition];
        }

        public void MoveBack()
        {
            PreviousWordIndex = CurrentWordIndex;
            CurrentWordPosition = CurrentWordPosition - 1 < 0 ? _matchIndexes.Count - 1 : CurrentWordPosition - 1;
            CurrentWordIndex = _matchIndexes[CurrentWordPosition];
        }

        public void ResetPosition()
        {
            CurrentWordPosition = 0;
            CurrentWordIndex = _matchIndexes[CurrentWordPosition];
            PreviousWordIndex = _matchIndexes[_matchIndexes.Count - 1];
        }
    }
}
