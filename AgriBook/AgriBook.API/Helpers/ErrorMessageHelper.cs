using System;
using System.Text;

namespace AgriBook.API.Helpers
{
    public class ErrorMessageHelper
    {
        private static StringBuilder _stringBuilder;

        public ErrorMessageHelper()
        {
            if (_stringBuilder == null)
            {
                _stringBuilder = new StringBuilder();
            }
        }

        public string GetErrorMessage(Exception exception)
        {
            _stringBuilder.AppendLine("Error message: " + exception.Message);
            _stringBuilder.AppendLine("Stack trace: " + exception.StackTrace);

            var exceptionErrorMessage = _stringBuilder.ToString();
            _stringBuilder.Clear();

            return exceptionErrorMessage;
        }
    }
}