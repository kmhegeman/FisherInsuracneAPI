using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FisherInsuranceApi.Data;

namespace FisherInsuranceApi.Migrations
{
    [DbContext(typeof(FisherContext))]
    [Migration("20170303201241_updateQuotes")]
    partial class updateQuotes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("FisherInsuranceApi.Models.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("LossAmount");

                    b.Property<DateTime>("LossDate");

                    b.Property<string>("PolicyNumber");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("Claim");
                });

            modelBuilder.Entity("FisherInsuranceApi.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpireDate");

                    b.Property<decimal>("Price");

                    b.Property<string>("Product");

                    b.HasKey("Id");

                    b.ToTable("Quote");
                });
        }
    }
}
