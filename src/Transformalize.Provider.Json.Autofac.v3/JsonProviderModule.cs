﻿#region license
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

using Autofac;
using System.IO;
using Transformalize.Configuration;

namespace Transformalize.Providers.Json.Autofac {
   public class JsonProviderModule : Module {
      private readonly Process _process;
      private readonly StreamWriter _streamWriter;
      public bool UseAsyncMethods { get; set; }
      public JsonProviderModule() { }

      public JsonProviderModule(Process process, StreamWriter streamWriter = null) {
         _process = process;
         _streamWriter = streamWriter;
      }

      protected override void Load(ContainerBuilder builder) {

         if (_process == null)
            return;

         var b = new JsonProviderBuilder(_process, builder, _streamWriter) { UseAsyncMethods = UseAsyncMethods };

         b.Build();        

      }
   }
}