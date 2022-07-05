--1) 
SELECT *
FROM [Test].[Contracts] C JOIN [Test].[Accounts] A ON C.Id = A.Contract_Id
WHERE 
((C.[DateFrom] NOT BETWEEN A.[DateTimeFrom] AND A.[DateTimeTo]) 
	OR (C.[DateTo] NOT BETWEEN A.[DateTimeFrom] AND A.[DateTimeTo]))
OR (C.[DateFrom] > C.[DateTo])
OR (C.[DateTo] IS NULL AND A.DateTimeTo IS NOT NULL)

--2)
DECLARE @WithOutDepart_Id Int = 2

;WITH children AS(
  SELECT D.Id
  FROM [Test].[Departs] D
  WHERE Id = @WithOutDepart_Id
  UNION ALL
  SELECT D.Id
  FROM [Test].[Departs] D
	JOIN children
	ON D.Parent_Id = children.Id
)

SELECT D.*
FROM [Test].[Departs] D
WHERE D.Id NOT IN (SELECT C.Id FROM children C)


--3)
exec ('select ? = ISNULL(change_tracking_current_version(),0)', @ChangeTrackingCurrentVersion output) at [serv1]
--На сервере [serv1] будет выполнен select, который вернет в ouput параметре либо версию отслеживания изменений, либо 0.

--4)
--====================================================|==========================================================
--КЛАСТЕРНЫЕ ИНДЕКСЫ								  |	НЕКЛАСТЕРНЫЕ ИНДЕКСЫ
--====================================================|==========================================================
--Кластерный идекс может быть только 1.				  |	Некластерных индексов может быть много.
--Кластерный инлекс хранит в листьях строки данных.	  |	Некластерный индекс хранит указатель на строки в таблице.
--Кластерный индекс поддерживает сортировку значений. |	Некластерный индекс не поддерживает сортировку.

--5)
--===========|==================================|=======================================|===================================
--			 |	Таблице без индексов (куча)	    |	Таблице с некластерным индексом	на	|	Таблице с кластерным индексом на
--			 |								    |	столбце x							|	столбце x
--			 |==================================|=======================================|==================================
--			 |	SELECT *  |	SELECT x  |	WHERE x | SELECT *    |	SELECT	x |	 WHERE		|	SELECT *  |	SELECT x  |	WHERE x
--===========|============|===========|=========|=============|===========|=============|=============|===========|=========
--TABLE SCAN |		+	  |		+	  |	  +	    |		+	  |			  |		+  		|			  |			  |		  									
--INDEX SCAN |			  |			  |		    |			  |		+	  |		 		|		+	  |		+	  |		  				
--INDEX SEEK |			  |			  |		    |			  |			  |		  		|			  |			  |		+ 			
--

--6)
--Для увеличения скорости выполнения запроса небходимо:
--	Надо изменить блок WHERE
--	Добавить некластерные индексы на поля d.Number и c.Code

Select
	d.DocDate
From
	Document d
	Inner join Contract c on c.ContractID = d.documentID
Where
	d.Number = 79934356368 AND c.Code = 'ПП'

CREATE NONCLUSTERED INDEX IX_Document_Number ON Document (Number); 
CREATE NONCLUSTERED INDEX IX_Contract_Code ON Contract (Code); 
