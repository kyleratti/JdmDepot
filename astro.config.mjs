import { defineConfig } from "astro/config";
import yaml from "@rollup/plugin-yaml";
import tailwindcss from "@tailwindcss/vite";

// https://astro.build/config
export default defineConfig({
  vite: {
    plugins: [yaml(), tailwindcss()],
  },
});