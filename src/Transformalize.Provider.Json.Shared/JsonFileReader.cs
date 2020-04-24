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
using Transformalize.Context;
using Transformalize.Contracts;

namespace Transformalize.Providers.Json {

   public class JsonFileReader : IRead {

      private readonly InputContext _context;
      private readonly IRead _streamWriter;
      private readonly FileStream _stream;

      public JsonFileReader(InputContext context, IRowFactory rowFactory) {
         _context = context;
         var fileInfo = FileUtility.Find(_context.Connection.File);
         _stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
         _streamWriter = new JsonStreamReader(context, _stream, rowFactory);
      }

      public IEnumerable<IRow> Read() {
         return _streamWriter.Read();
      }
   }
}