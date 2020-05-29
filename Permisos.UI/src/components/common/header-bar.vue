<template>
  <nav
    class="navbar has-background-dark is-dark"
    role="navigation"
    aria-label="main navigation"
  >
    <HeaderBarBrand></HeaderBarBrand>
    <HeaderBarLinks></HeaderBarLinks>
  </nav>
</template>

<script>
import HeaderBarBrand from '@/components/common/header-bar-brand';
import HeaderBarLinks from '@/components/common/header-bar-links';

export default {
  name: 'HeaderBar',
  components: { HeaderBarBrand, HeaderBarLinks }
};
</script>
