import { useEffect, useMemo, useState } from 'react'
import { BarChart } from '@mui/x-charts'
import { DataGrid, type GridColDef } from '@mui/x-data-grid'
import './App.css'

type ProductListData = any

function App() {

  const [productList, setProductList] = useState<ProductListData>([])
  const [productQuantityList, setProductQuantityList] = useState([])

  const columns: GridColDef<ProductListData>[] = [
    { field: 'productCode', headerName: 'ID', width: 120 },
    {
      field: 'name',
      headerName: 'Product Name',
      width: 400,
    },
    {
      field: 'price',
      headerName: 'Price',
      type: 'number',
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
    },
    {
      field: 'createdAt',
      headerName: 'Date Added',
      type: 'string',
      width: 200,
      valueFormatter: (value) => new Date(value).toDateString(),
    },
  ]

  const xAxis = useMemo(() => productQuantityList.map((c) => c.category), [productQuantityList])
  const chartQuantityList = useMemo(
    () => productQuantityList.map((c) => c.quantity),
    [productQuantityList],
  )

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
      <h1>Product Management Dashboard</h1>
      <h2>List of Products</h2>
      <DataGrid<ProductListData>
        rows={productList}
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
