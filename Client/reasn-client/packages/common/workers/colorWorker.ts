self.onmessage = function (event) {
  const { imageData } = event.data;

  const colorDistance = (color1: string, color2: string): number => {
    const [r1, g1, b1] = color1.split(",").map(Number);
    const [r2, g2, b2] = color2.split(",").map(Number);
    return Math.sqrt((r1 - r2) ** 2 + (g1 - g2) ** 2 + (b1 - b2) ** 2);
  };

  const colorCount: { [key: string]: number } = {};
  const incrementValue = 120;

  for (let i = 0; i < imageData.length; i += incrementValue) {
    const r = imageData[i];
    const g = imageData[i + 1];
    const b = imageData[i + 2];

    const color = `${r},${g},${b}`;
    if (colorCount[color]) {
      colorCount[color]++;
    } else {
      colorCount[color] = 1;
    }
  }

  let sortedColors = Object.entries(colorCount).sort(
    ([, countA], [, countB]) => countB - countA,
  );

  sortedColors = sortedColors.filter(([color]) => {
    const [r, g, b] = color.split(",").map(Number);
    return (r > 5 || g > 5 || b > 5) && (r < 250 || g < 250 || b < 250);
  });

  sortedColors = sortedColors.reduce(
    (acc: [string, number][], [color, count]) => {
      const similarColor = acc.find(
        ([accColor]) => colorDistance(color, accColor) < 10,
      );

      if (!similarColor) {
        acc.push([color, count]);
      } else {
        similarColor[1] += count;
      }

      return acc;
    },
    [],
  );

  const dominantColors = sortedColors.slice(0, 10).map(([color]) => color);
  postMessage({ dominantColors });
};

export {};
