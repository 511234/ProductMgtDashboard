import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    host: true,
    proxy: {
      '/api': {
        target: 'http://product-mgt-dashboard-be:8001',
        changeOrigin: true,
        secure: false,
      },
    },
  },
})
