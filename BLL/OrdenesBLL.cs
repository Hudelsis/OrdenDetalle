using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using OrdenDetalle.Entidades;
using OrdenDetalle.DAL;
using System.Text;

namespace OrdenDetalle.BLL
{
   class OrdenesBLL
    {
        public static bool Guardar(Ordenes ordene)
        {
            if (!Existe(ordene.OrdenId))
                return Insertar(ordene);
            else
                return Modificar(ordene);
        }

        private static bool Insertar(Ordenes ordene)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Ordenes.Add(ordene);
                paso = contexto.SaveChanges() > 0;
                
                Productos producto;
                List<OrdenesDetalle> detalle = ordene.Detalle;
                foreach (OrdenesDetalle d in detalle)
                {
                    producto = ProductosBLL.Buscar(d.ProductoId);
                    producto.Inventario += d.Cantidad;
                    ProductosBLL.Guardar(producto);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        private static bool Modificar(Ordenes ordene)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                Productos producto;
                List<OrdenesDetalle> detalle = Buscar(ordene.OrdenId).Detalle;
                foreach (OrdenesDetalle d in detalle)
                {
                    producto = ProductosBLL.Buscar(d.ProductoId);
                    producto.Inventario -= d.Cantidad;
                    ProductosBLL.Guardar(producto);
                }
                contexto.Database.ExecuteSqlRaw($"Delete FROM OrdenesDetalle Where OrdenId={ordene.OrdenId}");
                foreach (var item in ordene.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                List<OrdenesDetalle> nuevo = ordene.Detalle;
                foreach (OrdenesDetalle d in nuevo)
                {
                    producto = ProductosBLL.Buscar(d.ProductoId);
                    producto.Inventario += d.Cantidad;
                    ProductosBLL.Guardar(producto);
                }

                contexto.Entry(ordene).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var ordene = OrdenesBLL.Buscar(id);
                Productos producto;
                List<OrdenesDetalle> viejosDetalles = Buscar(ordene.OrdenId).Detalle;
                foreach (OrdenesDetalle h in viejosDetalles)
                {
                    producto = ProductosBLL.Buscar(h.ProductoId);
                    producto.Inventario -= h.Cantidad;
                    ProductosBLL.Guardar(producto);
                }
                if (ordene != null)
                {
                    contexto.Entry(ordene).State = EntityState.Deleted;
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static Ordenes Buscar(int id)
        {
            Ordenes ordene = new Ordenes();
            Contexto contexto = new Contexto();

            try
            {
                ordene = contexto.Ordenes.Include(x => x.Detalle)
                    .Where(x => x.OrdenId == id)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return ordene;
        }

        public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> criterio)
        {
            List<Ordenes> Lista = new List<Ordenes>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Ordenes.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Ordenes.Any(e => e.OrdenId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }
    }
}
    
        
            
        
        