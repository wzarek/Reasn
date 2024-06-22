import { useState, useEffect } from "react";

function useColorWorker(imageData: Uint8ClampedArray | null | undefined) {
  const [dominantColors, setDominantColors] = useState<string[]>([]);

  useEffect(() => {
    if (!imageData) return;

    const worker = new Worker(
      new URL("../workers/colorWorker.ts", import.meta.url),
    );

    worker.onmessage = (event) => {
      setDominantColors(event.data.dominantColors);
      worker.terminate();
    };

    worker.postMessage({ imageData });

    return () => {
      worker.terminate();
    };
  }, [imageData]);

  return dominantColors;
}

export default useColorWorker;
