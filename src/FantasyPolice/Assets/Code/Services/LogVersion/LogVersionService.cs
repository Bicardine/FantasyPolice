using System.Text;
using UnityEngine;

namespace Services.LogVersion
{
    /// <summary>
    /// Used to write info about version on app launch to explore the logs.
    /// </summary>
    public class LogVersionService : ILogVersionService
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public void Do()
        {
            _stringBuilder.Clear();
            _stringBuilder.Append($"=== LogVersionService ===");
            _stringBuilder.Append($"Name: {Application.productName}");
            _stringBuilder.Append($"Version: {Application.version}");
            _stringBuilder.Append($"Platform: {Application.platform}");
            _stringBuilder.Append($"Unity version: {Application.unityVersion}");
            _stringBuilder.Append($"===========================");

            Debug.Log(_stringBuilder.ToString());
        }
    }
}