using System;

namespace LINQSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the Samples ViewModel
            SamplesViewModel vm = new SamplesViewModel
            {
                // Use Query or Method Syntax?
                UseQuerySyntax = false
            };

            // Call a sample method
            //vm.GetAllLooping();
            //vm.GetAll();
            //vm.GetSingleColumn();
            //vm.GetSpecificColumns();
            //vm.AnonymousClass();
            //vm.OrderBy();
            //vm.OrderByDescending();
            //vm.WhereExpression();
            //vm.WhereTwoFields();
            //vm.WhereExtensionMethod();
            //vm.First();
            //vm.FirstOrDefault();
            //vm.Last();
            //vm.LastOrDefault();
            //vm.Single();
            //vm.SingleOrDefault();
            //vm.ForEach();
            //vm.ForEachCallingMethod();
            //vm.Take();
            //vm.TakeWhile();
            //vm.Skip();
            //vm.SkipWhile();
            vm.Distinct();


            // Display Product Collection
            foreach (var item in vm.Products)
            {
                Console.Write(item.ToString());
            }

            // Display Result Text
            Console.WriteLine(vm.ResultText);
        }
    }
}
