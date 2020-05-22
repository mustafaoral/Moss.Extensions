﻿using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.HttpContentExtensions
{
    public class DeserializeAsStreamAsyncShould
    {
        [Fact]
        public async Task DeserializeContentAsStreamUsingDefaultJsonSerializerSettings()
        {
            await DeserializeContentAsStream(null).ConfigureAwait(false);
        }

        [Fact]
        public async Task DeserializeContentAsStreamUsingProvidedJsonSerializerSettings()
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await DeserializeContentAsStream(jsonSerializerOptions).ConfigureAwait(false);
        }

        private async Task DeserializeContentAsStream(JsonSerializerOptions jsonSerializerOptions)
        {
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString("N");

            var dto = new Dto
            {
                Id = id,
                Name = name
            };

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dto, jsonSerializerOptions))))
            {
                var response = new HttpResponseMessage
                {
                    Content = new StreamContent(ms)
                };

                var result = await response.Content.DeserializeAsStreamAsync<Dto>(jsonSerializerOptions).ConfigureAwait(false);

                result.Id.ShouldBe(id);
                result.Name.ShouldBe(name);
            }
        }

        private class Dto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}
