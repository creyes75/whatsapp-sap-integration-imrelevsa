/* 
SCRIPT DE RECONCILIACIÓN: Pagos Nuvei vs Facturas SAP B1
Objetivo: Identificar discrepancias entre lo cobrado en la pasarela y lo registrado en el ERP.
*/

SELECT 
    T0."DocNum" AS "Factura_SAP",
    T0."DocDate" AS "Fecha",
    T0."CardName" AS "Cliente",
    T0."DocTotal" AS "Total_SAP",
    T1."U_VoucherNum" AS "Referencia_Nuvei",
    T1."U_CreditSum" AS "Monto_Pagado",
    -- Calculamos la diferencia para encontrar errores de redondeo o cargos fallidos
    (T0."DocTotal" - T1."U_CreditSum") AS "Diferencia_Auditoria"
FROM OINV T0 -- Tabla de Facturas
INNER JOIN RCT3 T1 ON T0."DocEntry" = T1."DocEntry" -- Tabla de Pagos con Tarjeta (SAP)
WHERE T0."DocDate" >= '2026-01-01'
-- Solo mostrar registros donde la diferencia sea mayor a 1 centavo
AND ABS(T0."DocTotal" - T1."U_CreditSum") > 0.01
ORDER BY T0."DocDate" DESC;
