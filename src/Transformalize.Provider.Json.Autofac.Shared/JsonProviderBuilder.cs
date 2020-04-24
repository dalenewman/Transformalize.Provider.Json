using Autofac;
using System;
using System.IO;
using System.Linq;
using Transformalize.Configuration;
using Transformalize.Context;
using Transformalize.Contracts;
using Transformalize.Nulls;

namespace Transformalize.Providers.Json.Autofac {
   public class JsonProviderBuilder {

      private readonly ContainerBuilder _builder;
      private readonly Process _process;
      private readonly Stream _stream;

      public JsonProviderBuilder(Process process, ContainerBuilder builder, Stream stream = null) {
         _process = process ?? throw new ArgumentException("Json Provider Builder's constructor must be provided with a non-null process.", nameof(process));
         _builder = builder ?? throw new ArgumentException("Json Provider Builder's constructor must be provided with a non-null builder.", nameof(builder));
         _stream = stream;
      }

      public void Build() {
         // Json schema reading not supported yet
         foreach (var connection in _process.Connections.Where(c => c.Provider == "json")) {
            _builder.Register<ISchemaReader>(ctx => new NullSchemaReader()).Named<ISchemaReader>(connection.Key);
         }

         // Json input
         foreach (var entity in _process.Entities.Where(e => _process.Connections.First(c => c.Name == e.Connection).Provider == "json")) {

            // input version detector
            _builder.RegisterType<NullInputProvider>().Named<IInputProvider>(entity.Key);

            // input read
            _builder.Register<IRead>(ctx => {
               var input = ctx.ResolveNamed<InputContext>(entity.Key);
               var rowFactory = ctx.ResolveNamed<IRowFactory>(entity.Key, new NamedParameter("capacity", input.RowCapacity));
               return new JsonFileReader(input, rowFactory);
            }).Named<IRead>(entity.Key);

         }

         if (_process.Output().Provider == "json") {

            foreach (var entity in _process.Entities) {

               // ENTITY WRITER
               _builder.Register<IWrite>(ctx => {
                  var output = ctx.ResolveNamed<OutputContext>(entity.Key);
                  if (output.Connection.Stream && _stream != null) {
                     return new JsonStreamWriter(output, _stream);
                  } else {
                     return new JsonFileWriter(output);
                  }

               }).Named<IWrite>(entity.Key);
            }
         }

      }
   }
}
