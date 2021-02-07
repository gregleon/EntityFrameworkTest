using EntityFrameworkTest.Console.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EntityFrameworkTest.Console
{
    internal class Program
    {
        private static void AddRoot()
        {
            var context = new TestDbContext();
            var root = new Root()
            {
                DateChildren = new List<DateChild>()
                {
                    new DateChild()
                    {
                        // This doesn't have to be this exact date, it's just the one that allows bug reproduction
                        CreateDate = new DateTime(637480200495470000)
                    }
                },
                AnyEntity = new AnyEntity()
                {
                    AnyEntityParent = new AnyEntityParent()
                    {
                    }
                }
            };

            context.Roots.Add(root);
            context.SaveChanges();
        }

        private static void CopyRootToParent()
        {
            var context = new TestDbContext();
            var root = context.Roots.First();

            root.ParentRoot = new Root()
            {
                DateChildren = new List<DateChild>()
                {
                    new DateChild()
                    {
                        CreateDate = root.DateChildren.First().CreateDate
                    }
                },
                AnyEntityID = root.AnyEntityID
            };

            context.SaveChanges();
        }

        private static Root GetRoot(bool includeNonDateEntities, bool includeDateEntities)
        {
            var context = new TestDbContext();

            var queryRoots = context.Roots.AsQueryable();

            if (includeNonDateEntities)
            {
                queryRoots = queryRoots
                    .Include(o => o.AnyEntity.AnyEntityParent)
                    .Include(o => o.ParentRoot)
                    .Include(o => o.ParentRoot.AnyChildren)
                    .Include(o => o.AnyChildren);
            }

            if (includeDateEntities)
            {
                queryRoots = queryRoots.Include(o => o.DateChildren);
            }

            return queryRoots.First();
        }

        private static List<long> GetDateChildrenDateTicks(Root root)
        {
            // get datetime values from root and root parent 'date children' and convert them to ticks
            return root.DateChildren.Select(c => c.CreateDate.Ticks).Concat(root.ParentRoot.DateChildren.Select(c => c.CreateDate.Ticks)).ToList();
        }

        private static void TestLazyLoading(bool includeUnrelatedEntities)
        {
            var eagerLoaded = GetRoot(includeNonDateEntities: includeUnrelatedEntities, includeDateEntities: true);
            var eagerLoadedTicks = GetDateChildrenDateTicks(eagerLoaded);

            var lazyLoaded = GetRoot(includeNonDateEntities: includeUnrelatedEntities, includeDateEntities: false);
            var lazyLoadedTicks = GetDateChildrenDateTicks(lazyLoaded);

            if (lazyLoadedTicks.Distinct().Count() != eagerLoadedTicks.Distinct().Count())
            {
                var tickValues = lazyLoadedTicks.Concat(eagerLoadedTicks).Distinct();
                System.Console.WriteLine($@"Dates had different values and they were: {string.Join(", ", tickValues)}");
            }
            else
            {
                var tickValue = lazyLoadedTicks.Concat(eagerLoadedTicks).Distinct().Single();
                System.Console.WriteLine($@"Dates had a single value and it was: {tickValue}");
            }
        }

        private static void Main(string[] args)
        {
            var context = new TestDbContext();
            if (!context.Roots.Any())
            {
                AddRoot();
                CopyRootToParent();
            }

            System.Console.WriteLine(@"Running test without including non date entities:");
            // Test console output: Dates had a single value and it was: 637480200495470000
            TestLazyLoading(includeUnrelatedEntities: false);
            
            System.Console.WriteLine();
            
            System.Console.WriteLine(@"Running test when non date entities are included:");
            // Test console output: Dates had different values and they were: 637480200495470000, 637480200495466667
            TestLazyLoading(includeUnrelatedEntities: true);

            System.Console.ReadKey();
        }
    }
}