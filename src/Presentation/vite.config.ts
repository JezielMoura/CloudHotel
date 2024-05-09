import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
  plugins: [ react(), tailwindcss() ],
  root: './',
  publicDir: 'assets',
  build: {
    target: 'esnext',
    sourcemap: true,
    outDir: '../../spa',
    chunkSizeWarningLimit: 2000
  },
  server: {
    port: 3000,
    host: '0.0.0.0',
    https: {
      pfx: '/Users/moura/.aspnet/dev-certs/https/aspnetcore-localhost-82189636C550648379145FE34B64385931884357.pfx'
    },
    proxy: {
      '/api': {
        target: 'https://localhost:5000',
        secure: false
      }
    }
  }
})
