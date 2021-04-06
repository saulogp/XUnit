using Microsoft.EntityFrameworkCore;
using ProPendencia.Api.Controllers;
using ProPendencia.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace ProPendencia.Test
{
    public class PendenciaUnitTest
    {
        private DbContextOptions<PendenciaContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<PendenciaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new PendenciaContext(options))
            {
                context.Pendencia.Add(new Pendencia { Id = 1, Descricao = "Escovar o dente", Data = "Amanhã" });
                context.Pendencia.Add(new Pendencia { Id = 2, Descricao = "Escovar o dente do cachorro", Data = "hoje" });
                context.Pendencia.Add(new Pendencia { Id = 3, Descricao = "Ja fiz", Data = "ontem" });
                
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                IEnumerable<Pendencia> pendencias = pendenciaController.GetPendencia().Result.Value;

                Assert.Equal(3, pendencias.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                int pendenciaId = 2;
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pendencia = pendenciaController.GetPendencia(pendenciaId).Result.Value;
                Assert.Equal(2, pendencia.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Pendencia pendencia = new Pendencia()
            {
                Id = 4,
                Descricao = "Procastinar",
                Data = "depois de amanhã"
            };

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pend = pendenciaController.PostPendencia(pendencia).Result.Value;
                Assert.Equal(4, pendencia.Id);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Pendencia pendencia = new Pendencia()
            {
                Id = 4,
                Descricao = "Procastinar",
                Data = "bola"
            };

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pend = pendenciaController.PostPendencia(pendencia).Result.Value;
                Assert.Equal("bola", pendencia.Data);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pend = pendenciaController.DeletePendencia(2).Result.Value;
                Assert.Null(pend);
            }
        }

       


    }
}
