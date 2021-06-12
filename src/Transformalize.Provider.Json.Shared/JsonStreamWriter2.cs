#region license
// Transformalize
// Configurable Extract, Transform, and Load
// Copyright 2013-2019 Dale Newman
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Transformalize.Configuration;
using Transformalize.Context;
using Transformalize.Contracts;

namespace Transformalize.Providers.Json {

   public class JsonStreamWriter2 : IWrite {

      private readonly Stream _stream;
      private readonly Field[] _fields;
      private readonly string[] _formats;
      private readonly OutputContext _context;

      public JsonStreamWriter2(OutputContext context, Stream stream) {
         _context = context;
         _stream = stream;
         _fields = context.GetAllEntityOutputFields().ToArray();
         _formats = new string[_fields.Count()];
         for (int i = 0; i < _fields.Length; i++) {
            _formats[i] = _fields[i].Format == string.Empty ? string.Empty : string.Concat("{0:", _fields[i].Format, "}");
         }

      }

      public void Write(IEnumerable<IRow> rows) {

         var options = new JsonWriterOptions() { Indented = _context.Connection.Format == "json" };

         var jw = new Utf8JsonWriter(_stream, options);

         jw.WriteStartArray();

         foreach (var row in rows) {

            jw.WriteStartObject();

            for (int i = 0; i < _fields.Length; i++) {

               if (_formats[i] == string.Empty) {
                  switch (_fields[i].Type) {
                     case "bool":
                     case "boolean":
                        jw.WriteBoolean(_fields[i].Alias, (bool)row[_fields[i]]);
                        break;
                     case "byte":
                        jw.WriteNumber(_fields[i].Alias, System.Convert.ToUInt32(row[_fields[i]]));
                        break;
                     case "byte[]":
                        jw.WriteBase64String(_fields[i].Alias, (byte[])row[_fields[i]]);
                        break;
                     case "date":
                     case "datetime":
                        jw.WriteString(_fields[i].Alias, (System.DateTime)row[_fields[i]]);
                        break;
                     case "decimal":
                        jw.WriteNumber(_fields[i].Alias, (decimal)row[_fields[i]]);
                        break;
                     case "double":
                        jw.WriteNumber(_fields[i].Alias, (double)row[_fields[i]]);
                        break;
                     case "float":
                     case "real":
                     case "single":
                        jw.WriteNumber(_fields[i].Alias, (float)row[_fields[i]]);
                        break;
                     case "guid":
                        jw.WriteString(_fields[i].Alias, (System.Guid)row[_fields[i]]);
                        break;
                     case "int":
                     case "int32":
                        jw.WriteNumber(_fields[i].Alias, (int)row[_fields[i]]);
                        break;
                     case "int64":
                     case "long":
                        jw.WriteNumber(_fields[i].Alias, (long)row[_fields[i]]);
                        break;
                     case "int16":
                     case "short":
                        jw.WriteNumber(_fields[i].Alias, (short)row[_fields[i]]);
                        break;
                     case "string":
                        jw.WriteString(_fields[i].Alias, (string)row[_fields[i]]);
                        break;
                     case "uint":
                     case "uint32":
                        jw.WriteNumber(_fields[i].Alias, (uint)row[_fields[i]]);
                        break;
                     case "uint16":
                     case "ushort":
                        jw.WriteNumber(_fields[i].Alias, (ushort)row[_fields[i]]);
                        break;
                     case "uint64":
                     case "ulong":
                        jw.WriteNumber(_fields[i].Alias, (ulong)row[_fields[i]]);
                        break;
                     default:  // char, object
                        jw.WriteString(_fields[i].Alias, row[_fields[i]].ToString());
                        break;
                  }

               } else {
                  jw.WriteString(_fields[i].Alias, string.Format(_formats[i], row[_fields[i]]));
               }
            }
            jw.WriteEndObject();
            _context.Entity.Inserts++;

            if (_context.Entity.Inserts % _context.Entity.InsertSize == 0) {
               jw.FlushAsync();
            }
         }

         jw.WriteEndArray();

         jw.FlushAsync();
      }
   }
}
