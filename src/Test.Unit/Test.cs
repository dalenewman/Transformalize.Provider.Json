using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transformalize.Configuration;
using Transformalize.Containers.Autofac;
using Transformalize.Contracts;
using Transformalize.Providers.Console;
using Transformalize.Transforms.Json.Autofac;

namespace Test.Unit {
   [TestClass]
   public class Test {
      [TestMethod]
      public void TestMethod1() {
         var cfg = @"<cfg name='test'>
   <entities>
      <add name='entity'>
         <rows>
            <add key='1' json='{ ""string"": ""problematic"", ""number"": 1, ""array"": [2,3,4], ""object"" : { ""sixPointFour"": 6.4} }' />
         </rows>
         <fields>
            <add name='key' type='int' primary-key='true' />
            <add name='json' length='max' />
         </fields>
         <calculated-fields>
            <add name='string' t='copy(json).jsonpath($.string)' />
            <add name='number' type='int' t='copy(json).jsonpath($.number)' />
            <add name='array' t='copy(json).jsonpath($.array)' />
            <add name='numberInArray' type='int' t='copy(json).jsonpath($.array[2])' />
            <add name='sixPoint4' type='decimal' t='copy(json).jsonpath($.object.sixPointFour)' />
            <add name='filter' t='copy(json).jsonpath($.array[?(@!=2)])' />
         </calculated-fields>
      </add>
   </entities>
</cfg>";
         var logger = new ConsoleLogger(LogLevel.Debug);
         using(var outer = new ConfigurationContainer(new JsonTransformModule()).CreateScope(cfg, logger)) {
            var process = outer.Resolve<Process>();
            using(var inner = new Container(new JsonTransformModule()).CreateScope(process, logger)) {
               var controller = inner.Resolve<IProcessController>();
               IRow[] output = controller.Read().ToArray();

               Assert.AreEqual(1, output.Length);

               Assert.AreEqual("problematic", output[0][process.Entities[0].CalculatedFields[0]]);
               Assert.AreEqual(1, output[0][process.Entities[0].CalculatedFields[1]]);
               Assert.AreEqual(@"[
  2,
  3,
  4
]", output[0][process.Entities[0].CalculatedFields[2]]);
               Assert.AreEqual(4, output[0][process.Entities[0].CalculatedFields[3]]);
               Assert.AreEqual(6.4M, output[0][process.Entities[0].CalculatedFields[4]]);
               Assert.AreEqual("3", output[0][process.Entities[0].CalculatedFields[5]]);
            }
         }
      }
   }
}
