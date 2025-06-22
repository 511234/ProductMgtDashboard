import { useEffect, useMemo, useState } from 'react'
import { BarChart } from '@mui/x-charts'
import { DataGrid } from '@mui/x-data-grid'
import "./App.css"

function App() {
  const PRODUCT_CATEGORY = {
    0: 'Food',
    1: 'Electronics',
    2: 'Clothes',
    3: 'Health',
  }

  const [productList, setProductList] = useState([])
  const [productQuantityList, setProductQuantityList] = useState([])
  const [chartQuantityList, setChartQuantityList] = useState([])
  const [xAxis, setXAsis] = useState([])
  const columns: any = [
    { field: 'productCode', headerName: 'ID', width: 120 },
    {
      field: 'name',
      headerName: 'Product Name',
      width: 400,
    },
    {
      field: 'price',
      headerName: 'Price',
      type: 'decimal',
      width: 150,
    },
    {
      field: 'stockQuantity',
      headerName: 'Quantity',
      type: 'number',
      width: 150,
    },
    {
      field: 'category',
      headerName: 'Category',
      type: 'string',
      width: 150,
      valueFormatter: (value) => PRODUCT_CATEGORY[value],
    },
    {
      field: 'createdAt',
      headerName: 'Date Added',
      type: 'string',
      width: 200,
      valueFormatter: (value) => new Date(value).toDateString(),
    },
  ]

  const rows = productList

  useMemo(() => {
    setXAsis(productQuantityList.map((c) => c.categoryName))
    setChartQuantityList(productQuantityList.map((c) => c.quantity))
  }, [productQuantityList])

  useEffect(() => {
    const getData = async () => {
      const res = await fetch('/api/Product')
      if (!res.ok) {
        throw new Error('cannot get products')
      }
      const productListData = await res.json()
      setProductList(productListData)
    }

    const getProductQuantityList = async () => {
      const res = await fetch('/api/Product/quantity/all')
      if (!res.ok) {
        throw new Error('cannot get product quantity list')
      }
      const productQuantityListData = await res.json()
      setProductQuantityList(productQuantityListData)
    }

    getData()
    getProductQuantityList()
  }, [])

  return (
    <>
      <div></div>
      <h2>List of Products</h2>
      <DataGrid
        rows={rows}
        columns={columns}
        getRowId={(row) => row.productCode}
        initialState={{
          pagination: {
            paginationModel: {
              pageSize: 10,
            },
          },
        }}
        pageSizeOptions={[10]}
        disableRowSelectionOnClick
      />
      <h2>Product Quantity By Category</h2>
      <BarChart
        xAxis={[
          {
            data: xAxis,
            colorMap: {
              type: 'ordinal',
              values: xAxis,
              colors: ['#70d6ff', '#ff70a6', '#ff9770', '#ffd670'],
            },
          },
        ]}
        series={[{ data: chartQuantityList }]}
        barLabel="value"
        height={300}
        width={500}
      />
    </>
  )
}

export default App
