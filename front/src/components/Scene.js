import React, { useRef } from "react";
import { Canvas, useFrame } from "@react-three/fiber";
import { OrbitControls, useGLTF } from "@react-three/drei";

const Model = () => {
  const { scene } = useGLTF("../models/modelb.glb");
  const modelRef = useRef();

  useFrame(() => {
    if (modelRef.current) {
      modelRef.current.rotation.x += 0.01; // eixo Y
    }
  });

  return (
    <primitive
      ref={modelRef}
      object={scene}
      position={[2, 0, 0]}
      scale={[27, 27, 27]}
      rotation={[150 * (Math.PI / 180), 0, 0]}
    />
  );
};

const Scene = () => {
  return (
    <Canvas
      style={{ height: "500px" }}
      camera={{ position: [1, -10, -0.3], fov: 50 }}
    >
      <ambientLight intensity={1.5} />
      <directionalLight position={[25, 50, 25]} />
      <Model />
      <OrbitControls minDistance={10} maxDistance={10} />
    </Canvas>
  );
};

export default Scene;
