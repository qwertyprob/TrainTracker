import type { NextConfig } from "next";

const nextConfig: NextConfig = {
    sassOptions: {
        additionalData: `$var: red;`,
        includePaths: ["src/styles"],
    },
};

export default nextConfig;
