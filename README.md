# SqlBatis

A fork of the Apache IBatisNet distribution which has been refactored and migrated to .NET Standard, as shown in the change log.

[![Build Status](https://rasitha.visualstudio.com/OSS/_apis/build/status/rasitha1.SqlBatis?branchName=master)](https://rasitha.visualstudio.com/OSS/_build/latest?definitionId=2&branchName=master)

## [Releases](RELEASE.md)

## Usage

1. Install `SqlBatis.DataMapper` nuget package
2. Register Sql Mapper in DI 
```csharp
services.AddSqlMapper(options => Configuration.GetSection("DB").Bind(options));
```
3. Configure properties in appsettings.json
```json
{
  "DB": {
    "Resource": "embedded://MyApp.DataAccess.Configuration.SqlMap.config, MyApp.DataAccess",
    "Parameters": {
      "connectionString" :  "-injected-" 
    } 
  }
}
```
3. Use `ISqlMapper` in Data Access objects
```csharp
public class CustomerDao : ICustomerDao
{	
	public CustomerDao(ISqlMapper mapper)
	{
		Mapper = mapper;
	}

	private ISqlMapper Mapper {get;}

	public IEnumerable<Customer> GetAll()
	{
		return Mapper.QueryForList<Customer>("Customers.GetAll", null);
	}
	
	public Customer GetById(int id)
	{
		return Mapper.QueryForObject<Customer>("Customers.GetById", id);	
	}

	public void Insert(Customer customer)
	{
		Mapper.Insert("Customers.Insert", customer);
	}

	public void Update(Customer customer)
	{
		Mapper.Update("Customers.Update", customer);
	}

	public void Delete(int id)
	{
		Mapper.Delete("Customers.Delete", id);
	}
}
```
4. Setup `Customers.xml` SQL map file (ebmedded or external)
```xml
<?xml version="1.0" encoding="UTF-8" ?>
<sqlMap namespace="Customers" 
xmlns="http://ibatis.apache.org/mapping" 
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" >
  <statements>       
  	<select id="GetAll" resultClass="Customer">
      <![CDATA[
        SELECT
          c.Id,
          c.Name,
          c.Status,
          c.Type
        FROM dbo.Customer c          
      ]]>
    </select>
	<select id="GetById" extends="GetAll" resultClass="Customer">
	  <![CDATA[
        WHERE c.Id = #value#
      ]]>
    </select>
	<insert id="Insert">
	  <![CDATA[
      INSERT INTO dbo.Customer (Id, Name, Status, Type) 
      VALUES(#Id#, #Name#, #Status#, #Type#)
      ]]>
    </insert>
	<update id="Update">
	  <![CDATA[
      UPDATE dbo.Customer
      SET
        Name = #Name#,
        Status = #Status#,
        Type = #Type
      WHERE Id = #Id#  
      ]]>
    </update>
	<delete id="Delete">
	  <![CDATA[
      DELETE dbo.Customer WHERE Id = #value#
      ]]>
      </delete>
  </statements>	
</sqlMap>
```
5. Setup `SqlMap.config` (embedded or external file)
```xml
<?xml version="1.0" encoding="utf-8"?>
<sqlMapConfig xmlns="http://ibatis.apache.org/dataMapper" 
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" >
	<settings>
		<setting useStatementNamespaces="true"/>
	</settings>
	
	<providers embedded="MyApp.DataAccess.Configuration.providers.config, MyApp.DataAccess"/>

	<database>
		<provider name="SqlServer"/>
		<dataSource name="Primary" connectionString="${connectionString}"/>
	</database>
  
	<alias>
		<typeAlias alias="Customer" type="MyApp.Model.Customer, MyApp.Model"/>
	</alias>
	
	<sqlMaps>
		<sqlMap embedded="MyApp.DataAccess.SqlMaps.Customers.xml, MyApp.DataAccess"/>
	</sqlMaps>
</sqlMapConfig>

```


## Building



## Test Setup

1. Requires LocalDB
2. Run DBCreation.sql and DataBase.sql to setup the database (see `setupdb.cmd`)
3. SqlServer tests run fine. (Oracle, MySql, PostgreSQL ignored)
