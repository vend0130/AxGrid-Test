using AxGrid;
using AxGrid.Base;
using SmartFormat;
using SmartFormat.Core.Extensions;
using SmartFormat.Extensions;

namespace Result.UniRxAndSmartformat.Code
{
    public class SmartFormatTest : MonoBehaviourExt
    {
        [OnStart]
        private void StartThis()
        {
            Test11();
        }

        private void Test1()
        {
            var data = new { Name = "Vlad", Login = "Vend" };
            string text = Smart.Format("My name is {Login}.", data);
            Log.Debug(text);
        }

        private void Test2()
        {
            int[] data = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            string text = Smart.Format("{0:list:|; }.", data);
            Log.Debug(text);
        }

        private void Test3()
        {
            var data = new[]
            {
                new { Name = "John", Gender = 0 },
                new { Name = "Mary", Gender = 1 }
            };
            string text = Smart.Format("{Name} commented on {Gender:choose:his|her} photo", data[1]);
            Log.Debug(text);
        }
        
        private void Test6()
        {
            string text = Smart.Format("{0:choose(1|2|3):Один|Два|Три|Больше трех}", 5);
            Log.Debug(text);
        }
        
        private void Test8()
        {
            var chooseFmt = Smart.Default.GetFormatterExtension<ChooseFormatter>()!;
            chooseFmt.SplitChar = ',';
            string text = Smart.Format("{0:choose(one,two,three):|1|,|2|,|3|,|??|}", "two");
            Log.Debug(text);
        }

        private void Test11()
        {
            Smart.Default.AddExtensions(new CustomFormat());

            Log.Debug(Smart.Format("{value:game(3d):2d}", new { value = true }));

            Log.Debug(Smart.Format("{value:emag(3d):2d}", new { value = false }));
        }
    }

    public class CustomFormat : IFormatter
    {
        public string[] Names { get; set; } = new string[] { "game", "emag" };
        
        public bool TryEvaluateFormat(IFormattingInfo formattingInfo)
        {
            var iCanHandleThisInput = formattingInfo.CurrentValue is bool;
            if (!iCanHandleThisInput)
                return false;

            formattingInfo.Write("Game ");
            if ((bool) formattingInfo.CurrentValue)
                formattingInfo.Write(formattingInfo.FormatterOptions);
            else
                formattingInfo.Write(formattingInfo.Format.GetLiteralText());

            return true;
        }
    }
}