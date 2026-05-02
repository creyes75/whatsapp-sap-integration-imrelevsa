using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    /// <summary>
    /// DTO principal para la ingesta de ventas omnicanal.
    /// Define el contrato de datos entre plataformas externas (WhatsApp/Jelou) 
    /// y el orquestador de SAP Business One.
    /// </summary>
    public class InputSalesAPI
    {
        // --- Datos Maestros del Cliente ---
        public string CardCode { get; set; }          // Código del Business Partner en SAP
        public string Comments { get; set; }          // Comentarios que se verán en la Orden/Factura

        // --- Estructura Transaccional ---
        public List<DocumentLine> DocumentLines { get; set; }     // Detalle de SKU, Cantidad y Precios
        public List<PaymentCreditCardSAP> PaymentCreditCards { get; set; } // Información del pago (Nuvei/Gateway)
        public ShippingInformation ShippingInfo { get; set; }     // Datos logísticos y de entrega
    }

    /// <summary>
    /// Representa cada línea de producto (Item) en los documentos de SAP.
    /// </summary>
    public class DocumentLine
    {
        public string ItemCode { get; set; }         // SKU del producto
        public double Quantity { get; set; }         // Cantidad solicitada
        public decimal Price { get; set; }           // Precio unitario
        public decimal DiscountPercent { get; set; } // Porcentaje de descuento aplicado
    }

    /// <summary>
    /// Datos capturados de la pasarela de pagos (Nuvei/Otros).
    /// Algunos valores se parametrizan en el backend por seguridad.
    /// </summary>
    public class PaymentCreditCardSAP
    {
        public string VoucherNum { get; set; }       // Número de autorización/comprobante
        public decimal CreditSum { get; set; }       // Monto total cobrado
        // Nota: Datos como 'CreditCard' o 'OwnerId' se manejan vía Web.config para evitar 
        // exposición de datos sensibles desde el front-end.
    }

    /// <summary>
    /// Información de última milla y logística de envío.
    /// </summary>
    public class ShippingInformation
    {
        public string IsHomeDelivery { get; set; }   // Flag de envío (Mapeado a U_EnvioDomicilio)
        public string City { get; set; }             // Ciudad de destino (Mapeado a U_Ciudad)
        public string Route { get; set; }            // Trayecto/Zona (Mapeado a U_Trayecto)
        public string TrackingNumber { get; set; }   // Número de guía (Mapeado a U_Num_GuiaGen)
    }
}
