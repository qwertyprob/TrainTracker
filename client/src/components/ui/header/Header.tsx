"use client";

import styles from "@/components/ui/header/header.module.scss";
import Image from "next/image";

export default function Header() {
  return (
    <header className={styles.header}>
     <div className={styles.container}>
        <div
          className={`${styles.trainLogo} ${styles.circle} shadow-lg`}
          onClick={() => {
            window.location.href = "/";
          }}
        >
          <Image
            className="img-logo"
            src="/img/header/track-small.png"
            alt="Logo"
            width={80}
            height={100}
            style={{ objectFit: "contain" }}
            priority
            
            />
        </div>

        <h1 className={styles.trainString}>
          Real-time <span className={styles.trainText}>Train Tracking</span>{" "}
          Service
        </h1>
      </div>
    </header>
  );
}
