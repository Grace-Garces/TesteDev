SELECT
    cap.Numero AS NumeroProcesso,
    p.Nome AS NomeFornecedor,
    cap.DataVencimento,
    NULL AS DataPagamento,
    (cap.Valor + cap.Acrescimo - cap.Desconto) AS ValorLiquido,
    'A Pagar' AS Status
FROM
    ContasAPagar AS cap
INNER JOIN
    Pessoas AS p ON cap.CodigoFornecedor = p.Codigo

UNION ALL

SELECT
    cpg.Numero AS NumeroProcesso,
    p.Nome AS NomeFornecedor,
    cpg.DataVencimento,
    cpg.DataPagamento, 
    (cpg.Valor + cpg.Acrescimo - cpg.Desconto) AS ValorLiquido,
    'Paga' AS Status 
FROM
    ContasPagas AS cpg
INNER JOIN
    Pessoas AS p ON cpg.CodigoFornecedor = p.Codigo

ORDER BY
    DataVencimento,
    NomeFornecedor;