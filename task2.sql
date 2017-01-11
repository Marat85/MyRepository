create table #t ( Id int, ProductId int, CustomerId int, DataCreated datetime)

insert into #t values(1,10,7,getdate()-10)
insert into #t values(2,10,7,getdate()-8)

insert into #t values(3,10,9,getdate()-8)
insert into #t values(4,15,9,getdate()-2)
insert into #t values(5,16,9,getdate()-7)
insert into #t values(6,15,9,getdate()-8)

insert into #t values(7,10,12,getdate()-3)
insert into #t values(8,15,12,getdate()-2)
insert into #t values(9,16,12,getdate()-1)
insert into #t values(10,10,12,getdate()-11)
insert into #t values(11,15,12,getdate()-12)
insert into #t values(12,16,12,getdate()-13)



-- select * from #t

-- этот запрос показывает детально

select *, count(*) over (partition by ProductId) as cnt from(
select ROW_NUMBER() over (partition by ProductId, CustomerId order by DataCreated) as RID, * from #t )t where t.RID = 1

-- запрос показывает продукт и количество случаев, когда он был первой покупкой клиента

select distinct t1.ProductId, t1.cnt from(
select *, count(*) over (partition by ProductId) as cnt from(
select ROW_NUMBER() over (partition by ProductId, CustomerId order by DataCreated) as RID, * from #t )t 
where t.RID = 1)t1