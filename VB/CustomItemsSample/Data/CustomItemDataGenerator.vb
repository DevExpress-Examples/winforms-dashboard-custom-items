Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Public Class CustomItemDataGenerator
	Private Const trillionMultiplier As Long = 1000000000000

	Private Const totalExportUSA As Double = 2.377
	Private Const totalExportGermany As Double = 2.004
	Private Const totalExportFrance As Double = 0.969
	Private Const totalExportItaly As Double = 0.6873
	Private Const totalExportUK As Double = 0.9019
	Private Const totalExportCanada As Double = 0.6188
	Private Const totalExportChina As Double = 2.49
	Private Const totalExportAustralia As Double = 0.4046
	Private Const totalExportMexico As Double = 0.49159
	Private Const totalExportJapan As Double = 1.084
	Private Const totalExportBrazil As Double = 0.2914

	Public Shared Function GetExportData() As List(Of Export)
		Return New List(Of Export) (New Export() {New Export("United States", "Canada", totalExportUSA, 0.17), New Export("United States", "Mexico", totalExportUSA, 0.16), New Export("United States", "China", totalExportUSA, 0.07), New Export("United States", "Japan", totalExportUSA, 0.05), New Export("Germany", "United States", totalExportGermany, 0.09), New Export("Germany", "France", totalExportGermany, 0.08), New Export("Germany", "China", totalExportGermany, 0.07), New Export("Germany", "Netherlands", totalExportGermany, 0.06), New Export("Germany", "United Kingdom", totalExportGermany, 0.06), New Export("Germany", "Italy", totalExportGermany, 0.05), New Export("Germany", "Poland", totalExportGermany, 0.05), New Export("Germany", "Austria", totalExportGermany, 0.05), New Export("France", "Germany", totalExportFrance, 0.14), New Export("France", "United States", totalExportFrance, 0.08), New Export("France", "Italy", totalExportFrance, 0.07), New Export("France", "Spain", totalExportFrance, 0.07), New Export("France", "Belgium", totalExportFrance, 0.07), New Export("France", "United Kingdom", totalExportFrance, 0.07), New Export("Italy", "Germany", totalExportItaly, 0.12), New Export("Italy", "France", totalExportItaly, 0.11), New Export("Italy", "United States", totalExportItaly, 0.10), New Export("Italy", "United Kingdom", totalExportItaly, 0.05), New Export("Italy", "Spain", totalExportItaly, 0.05), New Export("Italy", "Switzerland", totalExportItaly, 0.05), New Export("United Kingdom", "United States", totalExportUK, 0.15), New Export("United Kingdom", "Germany", totalExportUK, 0.10), New Export("United Kingdom", "China", totalExportUK, 0.07), New Export("United Kingdom", "Netherlands", totalExportUK, 0.07), New Export("United Kingdom", "France", totalExportUK, 0.07), New Export("United Kingdom", "Ireland", totalExportUK, 0.06), New Export("Canada", "United States", totalExportCanada, 0.73), New Export("China", "United States", totalExportChina, 0.17), New Export("China", "Hong Kong", totalExportChina, 0.10), New Export("China", "Japan", totalExportChina, 0.06), New Export("Australia", "China", totalExportAustralia, 0.39), New Export("Australia", "Japan", totalExportAustralia, 0.15), New Export("Australia", "South Korea", totalExportAustralia, 0.07), New Export("Australia", "India", totalExportAustralia, 0.05), New Export("Mexico", "United States", totalExportMexico, 0.75), New Export("Japan", "United States", totalExportJapan, 0.19), New Export("Japan", "China", totalExportJapan, 0.18), New Export("Japan", "South Korea", totalExportJapan, 0.06), New Export("Japan", "Taiwan", totalExportJapan, 0.06), New Export("Brazil", "China", totalExportBrazil, 0.28), New Export("Brazil", "United States", totalExportBrazil, 0.13)})
	End Function
	Public Shared Function GetContinentData() As List(Of CountryInformation)
		Return New List(Of CountryInformation) (New CountryInformation() {New CountryInformation("North America", "United States", "Washington", 38.8951, -77.0364), New CountryInformation("North America", "Canada", "Ottawa", 45.424721, -75.695000), New CountryInformation("North America", "Mexico", "Mexico City", 19.432608, -99.133209), New CountryInformation("Asia", "China","Beijing", 39.916668, 116.383331), New CountryInformation("Asia", "Hong Kong","Hong Kong", 22.302711,114.177216), New CountryInformation("Asia","Japan", "Tokyo", 35.652832,139.839478), New CountryInformation("Asia","South Korea", "Seoul", 37.532600,127.024612), New CountryInformation("Asia","Taiwan", "Taipei", 25.033964,121.564468), New CountryInformation("Asia","India", "New Delhi", 28.644800,77.216721), New CountryInformation("South America", "Brazil","Brasilia", -15.793889,-47.882778), New CountryInformation("Australia","Australia", "Canberra",-35.282001,149.128998), New CountryInformation("Europe","Netherlands","Amsterdam", 52.377956,4.897070), New CountryInformation("Europe","Germany","Berlin", 52.520008,13.381777), New CountryInformation("Europe","United Kingdom","London", 51.509865,-0.118092), New CountryInformation("Europe","Italy", "Rome", 41.902782,12.496366), New CountryInformation("Europe","France", "Paris", 48.864716,2.349014), New CountryInformation("Europe","Spain","Madrid", 40.416775,-3.703790), New CountryInformation("Europe","Poland","Warsaw ", 52.237049,21.017532), New CountryInformation("Europe","Austria","Vienna", 48.210033,16.363449), New CountryInformation("Europe","Belgium","Brussels", 50.8505,4.3488), New CountryInformation("Europe","Ireland","Dublin", 53.350140, -6.266155)})
	End Function
	Public Class Export
		Private privateExporter As String
		Public Property Exporter() As String
			Get
				Return privateExporter
			End Get
			Set(ByVal value As String)
				privateExporter = value
			End Set
		End Property
		Private privateImporter As String
		Public Property Importer() As String
			Get
				Return privateImporter
			End Get
			Set(ByVal value As String)
				privateImporter = value
			End Set
		End Property
		Private privateSum As Decimal
		Public Property Sum() As Decimal
			Get
				Return privateSum
			End Get
			Set(ByVal value As Decimal)
				privateSum = value
			End Set
		End Property

		Public Sub New(ByVal exporter As String, ByVal importer As String, ByVal exporterTotalExportValue As Double, ByVal shareOfExports As Double)
			Me.Exporter = exporter
			Me.Importer = importer
			Me.Sum = Convert.ToDecimal(exporterTotalExportValue * trillionMultiplier * shareOfExports)
		End Sub
	End Class
	Public Class CountryInformation
		Private privateCountry As String
		Public Property Country() As String
			Get
				Return privateCountry
			End Get
			Set(ByVal value As String)
				privateCountry = value
			End Set
		End Property
		Private privateContinent As String
		Public Property Continent() As String
			Get
				Return privateContinent
			End Get
			Set(ByVal value As String)
				privateContinent = value
			End Set
		End Property
		Private privateCapital As String
		Public Property Capital() As String
			Get
				Return privateCapital
			End Get
			Set(ByVal value As String)
				privateCapital = value
			End Set
		End Property
		Private privateCapitalLocation As GeoLocation
		Public Property CapitalLocation() As GeoLocation
			Get
				Return privateCapitalLocation
			End Get
			Set(ByVal value As GeoLocation)
				privateCapitalLocation = value
			End Set
		End Property

		Public Class GeoLocation
			Private privateLatitude As Double
			Public Property Latitude() As Double
				Get
					Return privateLatitude
				End Get
				Set(ByVal value As Double)
					privateLatitude = value
				End Set
			End Property
			Private privateLongitude As Double
			Public Property Longitude() As Double
				Get
					Return privateLongitude
				End Get
				Set(ByVal value As Double)
					privateLongitude = value
				End Set
			End Property

			Public Sub New(ByVal latitude As Double, ByVal longitude As Double)
				Me.Latitude = latitude
				Me.Longitude = longitude
			End Sub
		End Class
		Public Sub New(ByVal continent As String, ByVal country As String, ByVal capital As String, ByVal capitalLatitude As Double, ByVal capitalLongitude As Double)
			Me.Country = country
			Me.Continent = continent
			Me.Capital = capital
			Me.CapitalLocation = New GeoLocation(capitalLatitude, capitalLongitude)
		End Sub
	End Class
End Class
