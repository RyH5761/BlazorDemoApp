using System.Text.Json;

namespace BlazorDemoApp.Admin.Common
{
    public static class JsonHelper
    {
        public static T Load<T>(string relativePath) where T : new()
        {
            try
            {
                // wwwroot 기준 절대경로 계산
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

                if (!File.Exists(fullPath))
                    return new T();

                var json = File.ReadAllText(fullPath);
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new T();
            }
            catch
            {
                // 오류 발생 시 빈 객체 반환
                return new T();
            }
        }
    }
}
