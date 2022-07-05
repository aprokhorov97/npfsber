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
--�� ������� [serv1] ����� �������� select, ������� ������ � ouput ��������� ���� ������ ������������ ���������, ���� 0.

--4)
--====================================================|==========================================================
--���������� �������								  |	������������ �������
--====================================================|==========================================================
--���������� ����� ����� ���� ������ 1.				  |	������������ �������� ����� ���� �����.
--���������� ������ ������ � ������� ������ ������.	  |	������������ ������ ������ ��������� �� ������ � �������.
--���������� ������ ������������ ���������� ��������. |	������������ ������ �� ������������ ����������.

--5)
--===========|==================================|=======================================|===================================
--			 |	������� ��� �������� (����)	    |	������� � ������������ ��������	��	|	������� � ���������� �������� ��
--			 |								    |	������� x							|	������� x
--			 |==================================|=======================================|==================================
--			 |	SELECT *  |	SELECT x  |	WHERE x | SELECT *    |	SELECT	x |	 WHERE		|	SELECT *  |	SELECT x  |	WHERE x
--===========|============|===========|=========|=============|===========|=============|=============|===========|=========
--TABLE SCAN |		+	  |		+	  |	  +	    |		+	  |			  |		+  		|			  |			  |		  									
--INDEX SCAN |			  |			  |		    |			  |		+	  |		 		|		+	  |		+	  |		  				
--INDEX SEEK |			  |			  |		    |			  |			  |		  		|			  |			  |		+ 			
--

--6)
--��� ���������� �������� ���������� ������� ���������:
--	���� �������� ���� WHERE
--	�������� ������������ ������� �� ���� d.Number � c.Code

Select
	d.DocDate
From
	Document d
	Inner join Contract c on c.ContractID = d.documentID
Where
	d.Number = 79934356368 AND c.Code = '��'

CREATE NONCLUSTERED INDEX IX_Document_Number ON Document (Number); 
CREATE NONCLUSTERED INDEX IX_Contract_Code ON Contract (Code); 
